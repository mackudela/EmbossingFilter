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
            std::cout << "+";
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

        mask =
            (-1 * maskArray[north - 3]) + (0 * maskArray[north]) + (1 * maskArray[north + 3]) +
            (-1 * maskArray[middle - 3]) + (1 * maskArray[middle]) + (1 * maskArray[middle + 3]) +
            (-1 * maskArray[south - 3]) + (0 * maskArray[south]) + (1 * maskArray[south + 3]);
        mask /= 1;
        /*if (mask < 0) {
            mask = -mask;
        }
        if (mask > 255) {
            mask = mask - 255;
        }*/
        //std::cout << mask << std::endl;
        outputArray[startingPoint] = mask;
    }


        //int mask, middle, north, south, row, addition, maskRow = 0;

        //for (startingPoint; startingPoint < finishPoint; startingPoint++) {
        //    row = startingPoint / (3 * width);
        //    
        //    maskRow = startingPoint / (3 * maskWidth);

        //    if (row == maskRow && row != 0) {
        //        addition = (row - 1) * 2 * 3;
        //    }
        //    else {
        //        addition = row * 2 * 3;
        //    }

        //    middle = startingPoint + maskWidth + 3 + addition;

        //    north = middle - maskWidth; 
        //    south = middle + maskWidth; 

        //    /*mask =
        //        -maskArray[north - 3] + maskArray[north + 3] 
        //        -maskArray[middle - 3] + maskArray[middle] +
        //        maskArray[middle + 3] - maskArray[south - 3] +
        //        maskArray[south + 3];*/
        //    mask =
        //        maskArray[north - 3] + maskArray[north] + maskArray[north + 3] +
        //        maskArray[middle - 3] + maskArray[middle] + maskArray[middle + 3] +
        //        maskArray[south - 3] + maskArray[south] + maskArray[south + 3];
        //    mask /= 9;
        //    outputArray[startingPoint] = mask;
        //}
}
