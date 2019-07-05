pragma solidity >=0.4.24 <0.6.0;

contract IntTypes {

    function addition(uint248 _a, uint144 _b) public pure returns (uint256 c) {
        c = _a + _b;
        assert (c == _a + _b);
    }

    function subtraction(uint56 _a, uint8 _b) public pure returns (uint56 c) {
        c = _a - _b;
        assert (c == _a - _b);
    }

    function mutliplication(int56 _a, int8 _b) public pure returns (int64 c) {
        c = _a * _b;
        assert (c == _a * _b);
    }

    function division(uint248 _a, uint144 _b) public pure returns (uint256 c) {
        c = _a / _b;
        assert (c == _a / _b);
    }

}
