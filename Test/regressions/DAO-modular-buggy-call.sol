pragma solidity >=0.4.24<0.6.0;

// Shows the reentrancy bug without an explicit Mallory contract

contract SimpleDAO {
    mapping (address => uint) public credit;
    constructor() public {
    }
    function donate() payable public {
        credit[msg.sender] += msg.value;
    }
    function queryCredit(address to) public view returns (uint) {
        return credit[to];
    }
    function withdraw() public {
        uint oldBal = address(this).balance; 
        uint balSender = msg.sender.balance; // translated OK
        uint amount = credit[msg.sender];
        if (amount > 0) {
            bool success;
            bytes memory status;
            (success, status) = msg.sender.call.value(amount)(""); //VeriSol bug #22 (tuple declarations not handled in same declaration)
            require(success);
            credit[msg.sender] = 0;  // bug
        }
        uint bal = address(this).balance;
        assert(bal == oldBal || bal == (oldBal - amount));
    }
}
