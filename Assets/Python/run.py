#!/usr/bin/python

import sys
import json 
import numpy as np
import PIL
from PIL import Image
import time
import os.path
from os import path

# to run program: 
#   1: Place exported text file (from unity scene 'DevRecreateScene') into 'json2png_python/text_files' folder
#   2: cd into folder where this file is located
#   3: run 'python run.py [text_file]'
#   4: .png file should be saved into 'json2png_python/image_files' folder

# def main():

#     # # make sure argument exists
#     # if (len(sys.argv) <= 0):
#     #     print ("[ERROR] program required .txt file argument")
#     #     return -1

#     # # determine if all text files should be converted
#     # if (sys.argv[1] == "-a"):
#     #     for file in os.listdir("../json2png_python/text_files/"):
#     #         if file.endswith(".txt"):
#     #             print("converting new file: " + file)
#     #             createPNG(file)

#     # read in from python_arguments.py
#     python_arguments_file = open("python_arguments.txt")
#     python_arguments = python_arguments_file.read()
#     args = python_arguments.split(";")
#     print ("args: " + str(args))
#     createPNG(args)
    
#     print("closing program...")
#     return 1

def createPNG(args):

    # load in text from file file
    file = open(args[1], "r")
    file_text = file.read()

    # convert text to json
    file_json = json.loads(file_text)

    # extract data
    canvas_width = file_json["canvasSize"]["x"] * 16
    canvas_height = file_json["canvasSize"]["y"] * 16
    cell_data = file_json["cellData"]

    # Create a width x height x 3 array of 8 bit unsigned integers
    img_data = np.zeros((canvas_height, canvas_width, 3), dtype=np.uint8)

    # assign color to each pixel
    # print ("generating image...")
    for cell in cell_data:
        x = cell["pos"]["x"]
        y = cell["pos"]["y"]
        color_str = cell["colorHex"].lstrip('#')
        color_rgb = tuple(int(color_str[i:i+2], 16) for i in (0, 2, 4))
        img_data[canvas_height - y - 1, canvas_width - x - 1] = color_rgb 

    # create and save image
    img = Image.fromarray(img_data)
    img.save(args[2], format = "png")
    # print ("saving image @ " + args[2])
    # print ("\n")

python_arguments_file = open(path.curdir + "/python_arguments.txt")
python_arguments = python_arguments_file.read()
args = python_arguments.split(";")
# print ("args: " + str(args))
createPNG(args)


# if __name__ == "__main__":
#     main()