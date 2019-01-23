# python 3
import os
import argparse
import subprocess

parser = argparse.ArgumentParser(description='Optional app description')
parser.add_argument('input_folder', type=str,
                    help='A required string positional argument')
parser.add_argument('output_folder', type=str,
                    help='A required string positional argument')

args = parser.parse_args()
input_dir = args.input_folder
output_dir = args.output_folder

for filename in os.listdir(input_dir):
	input_path = input_dir + filename
	output_path = output_dir + filename[0:-3] + 'bpl'
	#print input_path
	#print output_path
	subprocess.run(['dotnet', 'Sources/SolToBoogie/bin/Debug/netcoreapp2.0/SolToBoogie.dll', input_path, './', output_path])