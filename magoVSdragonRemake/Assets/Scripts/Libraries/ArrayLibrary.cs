using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JordiArray;

namespace JordiArray
{
    internal static class ArrayLibrary
    {
        static int[] array = new int[1]; 

        internal static int[] GenerateIrrepetibleArray(int[] _array, int _minRange, int _maxRange)
        {
            array = _array;
            array = EraseIrrepetibleArray(_array.Length, _minRange);
            int newRandomValue;
            bool isRepeated;

            // loop array
            for(int i = 0; i < _array.Length; i++)
            {
                // generate new value
                newRandomValue = Random.Range(_minRange, _maxRange);

                //  repeat while new value is repeated
                do
                {            
                    // suppose isn't repeated
                    isRepeated = false;

                    // loop only assigned values
                    for(int j = 0; j < i; j++)
                    {
                        // if new random value == current
                        if(newRandomValue == array[j])
                        {               
                            // generate new value  
                            newRandomValue = Random.Range(_minRange, _maxRange);
                            
                            // tell is repeated
                            isRepeated = true;
                            
                            // not necessary check the rest
                            break;
                        }
                    }
                } while(isRepeated);

                //assign new value
                //Debug.Log("i= " + i);
                array[i] = newRandomValue;            
            }
            return array;
        }

        internal static int[] EraseIrrepetibleArray(int _length, int _minRange)
        {
            for(int i = 0; i < _length; i++)
            {
                array[i] = _minRange - 1;
            }
            return array;
        }
    }
}
