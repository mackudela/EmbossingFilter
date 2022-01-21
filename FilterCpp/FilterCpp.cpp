#include <cstddef>
#include <cstdint>
#include <iostream>
extern "C" __declspec(dllexport) void RunCpp(unsigned char* outputArray, unsigned char* maskArray, int startingPoint, int finishPoint, int width, int maskWidth) {
    
    /*int mask, maskG, maskB, middleR, middleG, middleB, southR, southB, southG,
        northR, northB, northG = 0;*/
    int mask, middle, north, south, row, addition, maskRow = 0;

    //int northWestR = startingPoint - width * 3 - 6 - width * 3 - 6 - 3; // north west R
    //int northWestG = northWestR + 1; // North west G
    //int northWestB = northWestR + 2; // North west B
    // output[0,0] -> mask[1,1]

    for (startingPoint; startingPoint < finishPoint; startingPoint++) {
        row = startingPoint / (3 * width);
        
        maskRow = startingPoint / (3 * maskWidth);

        if (row == maskRow && row != 0) {
            addition = (row - 1) * 2 * 3;
        }
        else {
            addition = row * 2 * 3;
        }




        middle = startingPoint + (width * 3) + 6 + 3 + addition;
        //middleG = middleR + 1;
        //middleB = middleR + 2;
        north = middle - (width * 3) - 6; // north west R
        //northG = northR + 1; // North west G
        //northB = northR + 2; // North west B
        south = middle + (width * 3) + 6; 
        //southB = southR + 1;
        //southG = southR + 2;

        /*mask =
            -maskArray[north - 3] + maskArray[north + 3] 
            -maskArray[middle - 3] + maskArray[middle] +
            maskArray[middle + 3] - maskArray[south - 3] +
            maskArray[south + 3];*/
        mask =
            maskArray[north - 3] + maskArray[north] + maskArray[north + 3] +
            maskArray[middle - 3] + maskArray[middle] + maskArray[middle + 3] +
            maskArray[south - 3] + maskArray[south] + maskArray[south + 3];
        mask /= 9;
        outputArray[startingPoint] = mask;
        //std::cout << mask;
    }
    
    
    
    
    
    
    
    /*int i = startingPoint;
    unsigned char temp;*/
    //uint8_t* output = new uint8_t;
    //for (; i < finishPoint; i += 3) {

    //    /////////////////////////////////////////////////////
    //    //swap R and B; raw_image[i + 1] is G, so it stays where it is.
    //    /*temp = byteArray[i + 0];
    //    byteArray[i + 0] = byteArray[i + 2];
    //    byteArray[i + 2] = temp;*/
    //}
}
