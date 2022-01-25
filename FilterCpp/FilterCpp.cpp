#include <cstddef>
#include <cstdint>
#include <iostream>
extern "C" __declspec(dllexport) void RunCpp(unsigned char* outputArray, unsigned char* maskArray, int startingPoint, int finishPoint, int width, int height) {
    
    int check, middle, north, south, mask = 0;
    for (startingPoint; startingPoint < finishPoint; startingPoint++) {
        if (startingPoint / (3 * width) == 0 ||
            startingPoint / (3 * width) == height - 1 || 
            startingPoint % (width * 3) == 0 || (startingPoint + 3) % (width * 3) == 0
            ) {
            startingPoint += 2;
            continue;
        }
        
        middle = startingPoint;
        north = middle - (width * 3);
        south = middle + (width * 3);


        mask =
            (0 * maskArray[north - 3]) + (0 * maskArray[north]) + (0 * maskArray[north + 3]) +
            (-1 * maskArray[middle - 3]) + (0 * maskArray[middle]) + (1 * maskArray[middle + 3]) +
            (0 * maskArray[south - 3]) + (0 * maskArray[south]) + (0 * maskArray[south + 3]);
        mask += 127;
        outputArray[startingPoint] = mask;
    }
}
