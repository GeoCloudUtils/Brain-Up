/*
    Author: Ghercioglo "Romeon0" Roman
 */

using Assets.Scripts.Framework.Other;
using Assets.Scripts.Screens;
using System;
using UnityEngine;

namespace Assets.Scripts.Games.Abstract
{
    public interface ControllerAbstract
    {
        bool EnableTimer { get; set; }

        void StartGame(Action<bool, bool> callback);
        void StopGame();
        bool Hint();
        ViewAbstract GetView();
        ModelAbstract GetModel();
    }


    //public class Model1 : ModelAbstract
    //{
    //    public void Create()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void StartGame()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void StopGame()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class View1 : ViewAbstract
    //{
    //    public Model1 GetModel()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public GameScreenAbstract GetScreen()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool Hint()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void SetModel(ModelAbstract model)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void StartGame(Action callback)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void StopGame()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    ModelAbstract ViewAbstract.GetModel()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    //public class Controller1 : ControllerAbstract
    //{
    //    public bool EnableTimer { get; set; }

    //    View1 view1;
    //    Model1 model1;

    //    void StartGame(Action<bool, bool> callback) { }
    //    void StopGame() { }
    //    bool Hint() { return true;}
    //    public ViewAbstract GetView() { return view1; }
    //    Model1 GetModel() { return model1; }

    //    void ControllerAbstract.StartGame(Action<bool, bool> callback)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void ControllerAbstract.StopGame()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    bool ControllerAbstract.Hint()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    //public T GetView<T, V>()
    //    //    where T : ViewAbstract<V>
    //    //    where V : ModelAbstract
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    ModelAbstract ControllerAbstract.GetModel()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class Test
    //{
    //    public void Hello()
    //    {
    //        new Controller1();
    //    }
    //}
}
