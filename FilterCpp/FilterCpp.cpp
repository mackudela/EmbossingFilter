#include <cstddef>
#include <cstdint>
extern "C" __declspec(dllexport) void RunCpp(unsigned char* byteArray, int startingPoint, int finishPoint) {
    int i = startingPoint;
    unsigned char temp;
    //uint8_t* output = new uint8_t;
    for (; i < finishPoint; i += 3) {
        //swap R and B; raw_image[i + 1] is G, so it stays where it is.
        temp = byteArray[i + 0];
        byteArray[i + 0] = byteArray[i + 2];
        byteArray[i + 2] = temp;
        /*output[i] = byteArray[i];
        output[i + 1] = byteArray[i + 1];
        output[i + 2] = byteArray[i + 2];*/
    }
    //return output;
    //return byteArray; RGB -> BGR
}

