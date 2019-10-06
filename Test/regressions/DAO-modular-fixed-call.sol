pragma solidity >=0.4.24<0.6.0;

import "./Libraries/VeriSolContracts.sol";

// this is a cleaned up example to show reentrancy attack without an explicit adversary
// to show the bug, comment (respectively uncomment) line with fix (respectively bug)
// use the file DAO-modular-fixed-call-w-prints.sol to see more values in trace

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
        VeriSol.Ensures(!(VeriSol.Old(credit[msg.sender]) == 0) || address(this).balance == VeriSol.Old(address(this).balance));

        uint oldBal = address(this).balance; 
        uint amount = credit[msg.sender];
        if (amount > 0) {
            credit[msg.sender] = 0;  // fix
            bool success;
            bytes memory status;
            (success, status) = msg.sender.call.value(amount)(""); //VeriSol bug #22 (tuple declarations not handled in same declaration)
            require(success);
            // credit[msg.sender] = 0;  // bug
        }
       uint bal = address(this).balance;
       assert(bal >= (oldBal - amount)); // making it == will fail since donate can be invoked in fallback        
    }
}
