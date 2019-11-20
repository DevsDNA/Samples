//
// Created by jorgedevsdna on 20/11/2019.
//


#include <jni.h>

JNIEXPORT jstring JNICALL
Java_com_example_helloclib_TestingCMethods_GetHello(JNIEnv *env, jclass clazz) {
    return (*env)->NewStringUTF(env, "Hello C world");
}