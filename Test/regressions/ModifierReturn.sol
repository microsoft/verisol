pragma solidity >=0.4.24 <0.6.0;

contract ModifierReturn {

    uint x;
    uint y;

    constructor (uint _x, uint _y) public {
        x = _x;
        y = _y;
    }

    modifier increment() {
        _;
        x = x + 1;
    }

    function wrapper() public {
        uint returnVal = plusOne();
        assert (returnVal == 10);
        assert (x == 11);
    }

    function plusOne() private increment returns (uint){
        require(x == 10);
        if (y > 10)
            return x;
        else
            return x;
    }

}
