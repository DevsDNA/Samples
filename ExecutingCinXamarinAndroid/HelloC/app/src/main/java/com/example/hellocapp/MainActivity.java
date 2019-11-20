package com.example.hellocapp;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;

import com.example.helloclib.TestingCMethods;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        String prueba = TestingCMethods.GetHello();

        setContentView(R.layout.activity_main);
    }
}
