# In the directory $ARG[0], finds .sol files and fixes 
# "pragma solidity" lines for compiler version 0.5.10

use File::Find; 

sub wanted 
{
    print "next file:\n";
	print $_;
	print "\n";
	@temp = substr($_, length($_)-4, 4);
	#print @temp;
	#print "\n";
    if (substr($_, length($_)-4, 4) eq ".sol")
    {
		print "found .sol file:\n";
		print $_;
		print "\n";
		@fileName = $_;
		#print "before open\n";
        open(IN, $_) or die "Can't open .sol";
		#print "after open\n";
        open(OUT,">","temp.out") or die "Can't open temp.out"; 
		#print "after 2nd open\n";
    while (<IN>)
    {
       @lines = split(" ");
	   #print "lines[0] is: ";
       #print @lines[0];
	   #print "\n";
	   
	   if ($lines[0] eq "pragma" && $lines[1] eq "solidity")
       {
	      print "found pragma\n";
          print OUT "pragma solidity >=0.4.24 <0.6.0;\n";
       }
       else
       {
		  #print "else\n";
          print OUT $_; 
       }
    }
	#print "after changes are done\n";
    close IN;
    close OUT;
	system("move temp.out @fileName");
   }    

}


find(\&wanted,$ARGV[0]); 