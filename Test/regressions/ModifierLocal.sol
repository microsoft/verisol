pragma solidity ^0.5.0;

contract ReentrancyGuard {
    /// @dev counter to allow mutex lock with only one SSTORE operation
    uint256 private _guardCounter;

    constructor () internal {
        // The counter starts at one to prevent changing it from zero to a non-zero
        // value, which is a more expensive operation.
        _guardCounter = 1; 
    }    

    function foo() public checkIncrement() {
        _guardCounter += 1;
    }

    modifier checkIncrement() {
        uint256 localCounter = _guardCounter;
        int x = 5;
        _;   
        int y = 10; 
        assert(localCounter != _guardCounter - 1); // should fail
        assert (x == y - 5); 
    }    
}
