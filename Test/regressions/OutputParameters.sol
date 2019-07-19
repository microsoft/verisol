pragma solidity >=0.4.24 <0.6.0;

contract OutputParameters {

    function unnamedReturn(uint _a) public returns (uint) {
        uint a = _a;
        assert (a == _a);
        return a;
    }

    function namedReturn(uint _a) public returns (uint a) {
        a = _a;
        assert (a == _a);
    }

    function mutipleReturns(uint _a, uint _b) public returns (uint o_sum, uint o_product) {
        o_sum = _a + _b;
        o_product = _a * _b;
        assert (o_sum == _a + _b);
        assert (o_product == _a * _b);
    }

}
