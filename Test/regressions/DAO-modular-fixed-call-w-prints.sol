pragma solidity >=0.4.24<0.6.0;

contract SimpleDAO {
    mapping (address => uint) public credit;
    constructor() public {
    }
    function donate() payable public {
        address sender = msg.sender;
        address dbgThis = address(this);
        uint value = msg.value;
        credit[msg.sender] += msg.value;
    }
    function queryCredit(address to) public view returns (uint) {
        return credit[to];
    }
    function withdraw() public {
        address sender = msg.sender;
        address dbgThis = address(this);

        uint oldBal = address(this).balance; 
        uint balSender = msg.sender.balance; // translated OK
        uint amount = credit[msg.sender];
        if (amount > 0) {
            credit[msg.sender] = 0;  // fix
            bool success;
            bytes memory status;
            (success, status) = msg.sender.call.value(amount)(""); //VeriSol bug #22 (tuple declarations not handled in same declaration)
            require(success);
        }
        uint dbgAmout = amount;
        uint dbgOldbal = oldBal;
        uint bal = address(this).balance;
        assert(bal == oldBal || bal >= (oldBal - amount)); // making it == will fail since donate can be invoked in fallback
    }
}
