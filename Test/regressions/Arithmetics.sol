pragma solidity >=0.4.24 <0.6.0;

contract Arithmetics {

    function addition(uint _a, uint _b) public pure returns (uint c) {
        c = _a + _b;
        assert (c == _a + _b);
    }

    function subtraction(uint _a, uint _b) public pure returns (uint c) {
        c = _a - _b;
        assert (c == _a - _b);
    }

    function mutliplication(uint _a, uint _b) public pure returns (uint c) {
        c = _a * _b;
        assert (c == _a * _b);
    }

    function division(uint _a, uint _b) public pure returns (uint c) {
        c = _a / _b;
        assert (c == _a / _b);
    }

}
