
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.XimgprocModule
{

    // C++: class SelectiveSearchSegmentationStrategyMultiple
    /**
     @brief Regroup multiple strategies for the selective search segmentation algorithm
     */

    public class SelectiveSearchSegmentationStrategyMultiple : SelectiveSearchSegmentationStrategy
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        ximgproc_SelectiveSearchSegmentationStrategyMultiple_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal SelectiveSearchSegmentationStrategyMultiple(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new SelectiveSearchSegmentationStrategyMultiple __fromPtr__(IntPtr addr) { return new SelectiveSearchSegmentationStrategyMultiple(addr); }

        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategyMultiple::addStrategy(Ptr_SelectiveSearchSegmentationStrategy g, float weight)
        //

        /**
         @brief Add a new sub-strategy
                                         @param g The strategy
                                         @param weight The weight of the strategy
         */
        public void addStrategy(SelectiveSearchSegmentationStrategy g, float weight)
        {
            ThrowIfDisposed();
            if (g != null) g.ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentationStrategyMultiple_addStrategy_10(nativeObj, g.getNativeObjAddr(), weight);


        }


        //
        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategyMultiple::clearStrategies()
        //

        /**
         @brief Remove all sub-strategies
         */
        public void clearStrategies()
        {
            ThrowIfDisposed();

            ximgproc_SelectiveSearchSegmentationStrategyMultiple_clearStrategies_10(nativeObj);


        }


#if (UNITY_IOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategyMultiple::addStrategy(Ptr_SelectiveSearchSegmentationStrategy g, float weight)
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentationStrategyMultiple_addStrategy_10(IntPtr nativeObj, IntPtr g_nativeObj, float weight);

        // C++:  void cv::ximgproc::segmentation::SelectiveSearchSegmentationStrategyMultiple::clearStrategies()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentationStrategyMultiple_clearStrategies_10(IntPtr nativeObj);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void ximgproc_SelectiveSearchSegmentationStrategyMultiple_delete(IntPtr nativeObj);

    }
}
