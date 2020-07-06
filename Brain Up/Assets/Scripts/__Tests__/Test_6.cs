using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InAppPurchaseController;

public class Test_6 : MonoBehaviour
{
    public async void Purchase()
    {
        PurchaseResult result = await InAppPurchaseController.Instance.Purchase("no_ads_test");

        if (result.processingResult.HasValue && !result.failureReason.HasValue)
        {
            Debug.Log("Item bought!");
        }
        else
            Debug.Log("Item NOT bought!");
    }
}
