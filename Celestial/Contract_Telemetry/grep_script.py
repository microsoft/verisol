import os, sys

print ("Path to directory containing sources: ")
directoryName = '"' + sys.argv[1] + '"'

cmd1 = "grep -l \"\.delegatecall\s(\|\.delegatecall(\" " + directoryName + "/* | wc -l"
cmd2 = "grep -l \"gasleft()\|msg\.gas\" " + directoryName + "/* | wc -l"
cmd3 = "grep -l \"assembly\" " + directoryName + "/* | wc -l"

os.system("echo 'Number of contracts that use 'delegatecall':'")
os.system(cmd1)
os.system("echo 'Number of contracts that use 'gasleft':'")
os.system(cmd2)
os.system("echo 'Number of contracts that use 'assembly':'")
os.system(cmd3)
