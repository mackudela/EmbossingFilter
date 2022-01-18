extern "C" __declspec(dllexport) int RunCpp(int a, int b) {
    int z = a + b;
    return z;
}

//private static byte[] getColorData(Bitmap bmp, bool reverseRGB)
//{
//    Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
//    BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
//
//    IntPtr ptr = bmpdata.Scan0;
//    int bytes = bmpdata.Stride * bmp.Height;
//    byte[] results = new byte[bytes];
//    Marshal.Copy(ptr, results, 0, bytes);
//
//    bmp.UnlockBits(bmpdata);
//    return results;
//}