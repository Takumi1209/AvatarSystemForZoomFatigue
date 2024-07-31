using OpenCVForUnity.CoreModule;
using System;
using System.Runtime.InteropServices;

#if !OPENCV_DONT_USE_UNSAFE_CODE
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
#endif

namespace OpenCVForUnity.UnityUtils
{
    public static class MatUtils
    {
        /**
        * Copies OpenCV Mat data to Pixel Data IntPtr.
        * <p>
        * <br>This method copies the OpenCV Mat data to the pixel data IntPtr.
        * <br>The pixel data must have the same byte size as the Mat data ([total() * elemSize()] byte).
        * <br>Because this method doesn't check bounds, is faster than Mat.get().
        * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
        *
        * @param mat a Mat object.
        * @param intPtr the pixel data must have the same byte size as the Mat data ([total() * elemSize()] byte).
        */
        public static void copyFromMat(Mat mat, IntPtr intPtr)
        {
            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            OpenCVForUnity_MatDataToByteArray(mat.nativeObj, intPtr);
        }

        /**
        * Copies Pixel Data IntPtr to OpenCV Mat data.
        * <p>
        * <br>This method copy the pixel data IntPtr to the OpenCV Mat data.
        * <br>The Mat object must have the same byte size as the pixel data ([total() * elemSize()] byte).
        * <br>Because this method doesn't check bounds, is faster than Mat.put().
        * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
        * 
        * @param intPtr a pixel data IntPtr.
        * @param mat the Mat object must have the same byte size as the pixel data ([total() * elemSize()] byte).
        */
        public static void copyToMat(IntPtr intPtr, Mat mat)
        {
            if (intPtr == IntPtr.Zero)
                throw new ArgumentException("intPtr == IntPtr.Zero");

            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            OpenCVForUnity_ByteArrayToMatData(intPtr, mat.nativeObj);
        }

        /**
        * Copies OpenCV Mat data to Pixel Data Array.
        * <p>
        * <br>This method copies the OpenCV Mat data to the pixel data Array.
        * <br>The pixel data Array must have the same byte size as the Mat data ([total() * elemSize()] byte).
        * <br>Because this method doesn't check bounds, is faster than Mat.get().
        * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
        *
        * @param mat a Mat object.
        * @param array the pixel data Array must have the same byte size as the Mat data ([total() * elemSize()] byte).
        */
        public static void copyFromMat<T>(Mat mat, T[] array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            if (array == null)
                throw new ArgumentNullException("array");

            unsafe
            {
                fixed (T* ptr = array)
                {
                    OpenCVForUnity_MatDataToByteArray(mat.nativeObj, (IntPtr)ptr);
                }
            }
        }

        /**
        * Copies Pixel Data Array to OpenCV Mat data.
        * <p>
        * <br>This method copies the pixel data Array to the OpenCV Mat data.
        * <br>The Mat object must have the same byte size as the pixel data Array ([total() * elemSize()] byte).
        * <br>Because this method doesn't check bounds, is faster than Mat.put().
        * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
        * 
        * @param array a pixel data Array.
        * @param mat the Mat object must have the same byte size as the pixel data Array ([total() * elemSize()] byte).
        */
        public static void copyToMat<T>(T[] array, Mat mat) where T : unmanaged
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            unsafe
            {
                fixed (T* ptr = array)
                {
                    OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, mat.nativeObj);
                }
            }
        }

#if !OPENCV_DONT_USE_UNSAFE_CODE
        /**
        * Copies OpenCV Mat data to Pixel Data NativeArray.
        * <p>
        * <br>This method copies the OpenCV Mat data to the pixel data NativeArray.
        * <br>The pixel data Array must have the same byte size as the Mat data ([total() * elemSize()] byte).
        * <br>Because this method doesn't check bounds, is faster than Mat.get().
        * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
        *
        * @param mat a Mat object.
        * @param array the pixel data Array must have the same byte size as the Mat data ([total() * elemSize()] byte).
        */
        public static void copyFromMat<T>(Mat mat, NativeArray<T> array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            unsafe
            {
                OpenCVForUnity_MatDataToByteArray(mat.nativeObj, (IntPtr)NativeArrayUnsafeUtility.GetUnsafePtr(array));
            }
        }

        /**
        * Copies Pixel Data NativeArray to OpenCV Mat data.
        * <p>
        * <br>This method copies the pixel data NativeArray to the OpenCV Mat data.
        * <br>The Mat object must have the same byte size as the pixel data Array ([total() * elemSize()] byte).
        * <br>Because this method doesn't check bounds, is faster than Mat.put().
        * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
        * 
        * @param array a pixel data Array.
        * @param mat the Mat object must have the same byte size as the pixel data Array ([total() * elemSize()] byte).
        */
        public static void copyToMat<T>(NativeArray<T> array, Mat mat) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            unsafe
            {
                OpenCVForUnity_ByteArrayToMatData((IntPtr)NativeArrayUnsafeUtility.GetUnsafeReadOnlyPtr(array), mat.nativeObj);
            }
        }
#endif

#if NET_STANDARD_2_1
        /**
         * * Copies OpenCV Mat data to Pixel Data NativeArray.
         * * <p>
         * * <br>This method copies the OpenCV Mat data to the pixel data NativeArray.
         * * <br>The pixel data Array must have the same byte size as the Mat data ([total() * elemSize()] byte).
         * * <br>Because this method doesn't check bounds, is faster than Mat.get().
         * * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
         * *
         * * @param mat a Mat object.
         * * @param array the pixel data Array must have the same byte size as the Mat data ([total() * elemSize()] byte).
         */
        public static void copyFromMat<T>(Mat mat, Span<T> array) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            unsafe
            {
                fixed (T* ptr = array)
                {
                    OpenCVForUnity_MatDataToByteArray(mat.nativeObj, (IntPtr)ptr);
                }
            }
        }

        /**
        * Copies Pixel Data NativeArray to OpenCV Mat data.
        * <p>
        * <br>This method copies the pixel data NativeArray to the OpenCV Mat data.
        * <br>The Mat object must have the same byte size as the pixel data Array ([total() * elemSize()] byte).
        * <br>Because this method doesn't check bounds, is faster than Mat.put().
        * <br>When mat.isContinuous() is false, this method will only copy if mat.dims() = 2. When mat.isContinuous() is true, there is no limit to the number of mat dimensions.
        * 
        * @param array a pixel data Array.
        * @param mat the Mat object must have the same byte size as the pixel data Array ([total() * elemSize()] byte).
        */
        public static void copyToMat<T>(Span<T> array, Mat mat) where T : unmanaged
        {
            if (mat == null)
                throw new ArgumentNullException("mat");
            if (mat != null)
                mat.ThrowIfDisposed();

            unsafe
            {
                fixed (T* ptr = array)
                {
                    OpenCVForUnity_ByteArrayToMatData((IntPtr)ptr, mat.nativeObj);
                }
            }
        }
#endif


#if (UNITY_IOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_MatDataToByteArray(IntPtr mat, IntPtr byteArray);

        [DllImport(LIBNAME)]
        private static extern void OpenCVForUnity_ByteArrayToMatData(IntPtr byteArray, IntPtr Mat);
    }
}