pragma solidity >=0.4.24 <0.6.0;

// tests bytesXX types
// Commented asserts repro translator bugs:
// - when casting bytesK to bytesN, where k < N, no padding with 0s is done (lines 36 and 42)
// - hex"33" != 0x33  (line 33) 
contract Bytes {
    bytes public data = "0x3333";  //dynamically sized array
    bytes1 public data1 = 0x33;
	bytes1 public d1 = hex"33";
	//bytes1 public data1 = "0x3"; 	
	bytes2 d;
	bytes2 public data2 = 0x1234;
	bytes3 public data3 = 0x0;
	bytes4 public data4;
	bytes32 public data32 = 0x0;	

	constructor () public {
		assert (data1 == 0x33);
		//assert (d1 == data1);              //fails (should pass)
		d = bytes2(data1);
		assert (d == data1);
		//assert (d == 0x3300);            //fails (should pass)
		//assert(data1[0] == d[0]);        //corral exception
		
		//From Solidity doc:
		bytes2 a = 0x1234;
	    bytes4 b = bytes4(a);              // b will be 0x12340000
		//assert (b == 0x12340000);         //fails (should pass)
        //assert(a[0] == b[0]);				//corral exception
        //assert(a[1] == b[1]);				//corral exception

		assert (data2 == 0x1234);
		assert (data3 == 0);
		data4 = "xyzw";
		assert (data4 == hex"78797a77");    //passes
		
		assert (data32 == 0);
	}
 
}
