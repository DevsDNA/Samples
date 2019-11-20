LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

LOCAL_MODULE := testingLib
LOCAL_SRC_FILES := testingLib.c

include $(BUILD_SHARED_LIBRARY)