pragma solidity >=0.4.24 <0.6.0;

contract InternalFunctionCall {

    function fooInt(uint x) internal returns (uint ret) {
        ret = x + 1;
    }
	
	function fooBool(bool b) internal returns (bool ret) {
        ret = !b;
    }
	
	function fooAddr(address a) internal returns (address ret) {
        ret = a;
    }

    function testInternalFunctionCall(uint par1, bool par2, address par3) public {
        uint res1 = fooInt(par1);
        assert (res1 == par1 + 1);
		
		bool res2 = fooBool(par2);
		assert (res2 == !par2);
		
		address res3 = fooAddr(par3);
		assert (res3 != par3);
    }

}
