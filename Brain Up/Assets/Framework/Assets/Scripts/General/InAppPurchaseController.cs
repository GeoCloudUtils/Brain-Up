//using Assets.Scripts.Framework.Other;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.Purchasing;

//public class InAppPurchaseController : Singleton<InAppPurchaseController>, IStoreListener
//{
//    //Vars
//    private IStoreController _controller;
//    private IExtensionProvider extensions;
//    private PurchaseFailureReason _initFailureReason;
//    InitializeState _initState = InitializeState.None;
//    private Dictionary<string, PurchaseResult> _tasks = new Dictionary<string, PurchaseResult>();

//    //Classes
//    public class PurchaseResult
//    {
//        public PurchaseFailureReason? failureReason;
//        public PurchaseProcessingResult? processingResult;
//        public PurchaseResult(PurchaseFailureReason failureReason) => this.failureReason = failureReason;
//        public PurchaseResult(PurchaseProcessingResult processingResult) => this.processingResult = processingResult;
//        public PurchaseResult() { }

//        public bool Processed => failureReason.HasValue || processingResult.HasValue;
//    }

//    public enum InitializeState
//    {
//        None = 0,
//        Initializing,
//        Success,
//        Fail
//    }



//    #region Public API

//    public async Task<PurchaseResult> Purchase(string productId)
//    {
//        //Initialize if need
//        if (_initState == InitializeState.None)
//        {
//            try
//            {
//                _initState = await InitPurchasingAsync().ConfigureAwait(true);
//                Debug.Log("Intialized. State: " + _initState);
//            }
//            catch (Exception e)
//            {
//                Debug.LogError(e);
//            }
//        }

//        //Try buy
//        try
//        {
//            PurchaseResult result = await BuyProductAsync(productId).ConfigureAwait(true);
//            if (result.processingResult.HasValue)
//                Debug.Log("Purchase finished. Result(processing): " + result.processingResult);
//            else
//                Debug.Log("Purchase finished. Result(error): " + result.failureReason);

//            return result;
//        }
//        catch (Exception e)
//        {
//            Debug.LogError(e);
//        }

//        return new PurchaseResult(PurchaseFailureReason.Unknown);
//    }

//    #endregion



//    #region Private Methods

//    private async Task<InitializeState> InitPurchasingAsync()
//    {
//        StandardPurchasingModule purchasingModule = StandardPurchasingModule.Instance(AppStore.GooglePlay);
//        ProductCatalog productCatalog = ProductCatalog.LoadDefaultCatalog();
//        ConfigurationBuilder configBuilder = ConfigurationBuilder.Instance(purchasingModule);

//        //Simulate user
//        //   purchasingModule.useFakeStoreUIMode = FakeStoreUIMode.DeveloperUser;
//        //  purchasingModule.useFakeStoreAlways = true;

//        Debug.Log("Initializing products...");
//        //Add all products
//        foreach (var product in productCatalog.allValidProducts)
//        {
//            Debug.LogFormat("Product[1]. Id: {0}; Type: {1}", product.id, product.type);
//            if (product.allStoreIDs.Count > 0)
//            {
//                var ids = new IDs();
//                foreach (var storeID in product.allStoreIDs)
//                {
//                    ids.Add(storeID.id, storeID.store);
//                }
//                configBuilder.AddProduct(product.id, product.type, ids);
//            }
//            else
//            {
//                configBuilder.AddProduct(product.id, product.type);
//            }
//        }
//        Debug.Log("Initializing products end.");

//        //Wait for Store result 
//        _initState = InitializeState.Initializing;
//        UnityPurchasing.Initialize(this, configBuilder);
//        while (_initState == InitializeState.Initializing)
//            await Task.Yield();

//        return _initState;
//    }

//    private async Task<PurchaseResult> BuyProductAsync(string productId)
//    {
//        while (_initState == InitializeState.Initializing)
//            await Task.Yield();

//        if (_initState == InitializeState.Fail)
//            return new PurchaseResult(PurchaseFailureReason.PurchasingUnavailable);

//        var purchaseResult = new PurchaseResult();
//        var product = _controller.products.WithID(productId);

//        if (product == null || product.availableToPurchase == false)
//        {
//            Debug.Log("BuyProductAsync: FAIL. Not purchasing product, either is not found or is not available for purchase");
//            return new PurchaseResult(PurchaseFailureReason.ProductUnavailable);
//        }

//        try
//        {
//            _tasks.Add(productId, purchaseResult);

//            _controller.InitiatePurchase(product);

//            while (!purchaseResult.Processed)
//                await Task.Yield();

//            return purchaseResult;
//        }
//        finally
//        {
//            _tasks.Remove(productId);
//        }

//    }

//    #endregion



//    #region Callbacks 

//    /// <summary> Called when Unity IAP is ready to make purchases. </summary>
//    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//    {
//        Debug.Log("OnInitSuccess called");

//        this._controller = controller;
//        this.extensions = extensions;

//        Debug.Log("Products---------------");
//        foreach (var product in controller.products.all)
//        {
//            Debug.LogFormat($"Title: {product.metadata.localizedTitle}; " +
//                $"Desc: {product.metadata.localizedDescription}; " +
//                $"Price: {product.metadata.localizedPriceString}");
//        }
//        Debug.Log("---------------------");
//        _initState = InitializeState.Success;
//    }


//    /// <summary> Called when initialization fails. Will try until success.  </summary>
//    public void OnInitializeFailed(InitializationFailureReason error)
//    {
//        Debug.Log("OnInitFailed called. Error: " + error.ToString());
//        _initState = InitializeState.Fail;
//    }

//    /// <summary> Called when a purchase succes.  </summary>
//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//    {
//        Debug.Log("OnPurchaseSuccess called.");
//        PurchaseResult result;
//        if (_tasks.TryGetValue(e.purchasedProduct.definition.id, out result))
//            result.processingResult = PurchaseProcessingResult.Complete;

//        return PurchaseProcessingResult.Complete;
//    }

//    /// <summary> Called when a purchase failed.  </summary>
//    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
//    {
//        Debug.Log("OnPurchaseFailed called.");
//        PurchaseResult result;
//        if (_tasks.TryGetValue(i.definition.id, out result))
//            result.failureReason = p;
//    }

//    #endregion



//}
