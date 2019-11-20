[assembly: Xamarin.Forms.Dependency(typeof(XamarinCApp.Droid.CcodeCaller))]
namespace XamarinCApp.Droid
{
    using Com.Example.Helloclib;
    using System;

    public class CcodeCaller : ICcodeCaller
    {
        public string GetHelloC()
        {
            return TestingCMethods.Hello;
        }
    }
}