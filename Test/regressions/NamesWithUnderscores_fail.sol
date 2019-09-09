pragma solidity >=0.4.24 <0.6.0;

contract Bar_1_1 {

    constructor () public { }
    
    function baz_1_1_1(uint p) public pure returns (uint ret) {
        ret = p + 2;
    }
    
}
contract ExternalFunctionCall_2_2 {

    function foo_2_1(uint x, Bar_1_1 y) public pure returns (uint ret) {
        ret = x + y.baz_1_1_1(2);
    }

    function testExternalFunctionCall_2_1(uint x) public returns (uint y) {
        Bar_1_1 bar = new Bar_1_1();
        y = this.foo_2_1(x, bar);
        assert (y != x + 4);
    }

}