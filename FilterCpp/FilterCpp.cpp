#include <cstddef>
#include <cstdint>
#include <iostream>
extern "C" __declspec(dllexport) void RunCpp(unsigned char* outputArray, unsigned char* maskArray, int startingPoint, int finishPoint, int width, int height) {
    
    int check, middle, north, south, mask = 0;
    for (startingPoint; startingPoint < finishPoint; startingPoint++) {
        //check = (startingPoint / 3) / width;
        //std::cout << "startingPoint: " << startingPoint << " finishPoint: " << finishPoint << " width: " << width << " height: " << height << std::endl;
        if (startingPoint / (3 * width) == 0 ||
            startingPoint / (3 * width) == height - 1 || 
            startingPoint % (width * 3) == 0 || (startingPoint + 3) % (width * 3) == 0
            ) {
            //std::cout << "+";
            startingPoint += 2;
            continue;
        }
        
        /*if (check == 0 || startingPoint % (3 * width) == 0 ||
            (startingPoint + 3) % (3 * width) == 0 || 
            check == height - 1) {
            startingPoint += 2;
            continue;
        }*/
        middle = startingPoint;
        north = middle - (width * 3);
        south = middle + (width * 3);
        //std::cout << "StartingPoint: " << startingPoint << " width*3: " << width * 3 << " north: " << north
        //    << " south: " << south << std::endl;

        mask =
            (0 * maskArray[north - 3]) + (0 * maskArray[north]) + (0 * maskArray[north + 3]) +
            (-1 * maskArray[middle - 3]) + (0 * maskArray[middle]) + (1 * maskArray[middle + 3]) +
            (0 * maskArray[south - 3]) + (0 * maskArray[south]) + (0 * maskArray[south + 3]);
        mask += 127;
        //mask /= 1;
        //std::cout << mask << std::endl;
        outputArray[startingPoint] = mask;
    }
}
