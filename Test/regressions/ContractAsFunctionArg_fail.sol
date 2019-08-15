pragma solidity >=0.4.24 <0.6.0;

contract Bar {

    constructor () public { }
    
    function baz(uint p) public pure returns (uint ret) {
        ret = p + 2;
    }
    
}
contract ExternalFunctionCall {

    function foo(uint x, Bar y) public pure returns (uint ret) {
        ret = x + y.baz(2);
    }

    function testExternalFunctionCall(uint x) public returns (uint y) {
        Bar bar = new Bar();
        y = this.foo(x, bar);
        assert (y != x + 4);
    }

}