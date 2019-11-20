package com.example.helloclib;

public class TestingCMethods {

    public static native String GetHello();

    static {
        System.loadLibrary("testingLib");
    }
}
