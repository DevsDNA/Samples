cmake_minimum_required(VERSION 3.5.2)

add_library(
    testingLib
    SHARED
    src/main/jni/testingLib.c
)

include_directories (
    src/main/jni/
)