pragma solidity >=0.4.24 <0.6.0;

contract A {

    constructor() public
    {
	    uint x=0;
		bool tmp= foo(x);  
	    if (tmp)
		    return;
		x =1;
		if (foo(x))
			return;
		assert(false);
    }

    function foo(uint x) internal returns (bool) {
        if (x == 1) {
            return false;
        }
		else
		    return false;
	}

  
}