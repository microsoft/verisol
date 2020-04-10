pragma solidity >=0.4.24 <0.6.0;

contract InternalFunctionCall {

	struct Counter {
		uint value;  //default: 0
	}
	
    function foo(uint x) internal returns (uint ret) {
        ret = x + 1;
    }

    function testInternalFunctionCall(uint x) public {
		Counter memory counter;
		uint y = foo(counter.value);
        assert (y == counter.value + 1);
    }
}
