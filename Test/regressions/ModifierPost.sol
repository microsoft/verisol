pragma solidity >=0.4.24 <0.6.0;

contract ModifierPost {

    uint x;

    constructor (uint _x) public {
        x = _x;
    }

    modifier onlyGreaterThanTen() {
        _;
        require(x > 10, 'Invalid x!');
    }

    function wrapper() public {
        plusOne();
        assert (x > 10);
    }

    function plusOne() private onlyGreaterThanTen {
        x = x - 1;
    }

}
