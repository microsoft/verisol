pragma solidity >=0.4.24<0.6.0;

import "./Libraries/VeriSolContracts.sol";

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
        VeriSol.Ensures (address(this).balance >= VeriSol.Old(address(this).balance - credit[msg.sender]));        

        uint oldBal = address(this).balance; 
        uint amount = credit[msg.sender];
        if (amount > 0) {
            // credit[msg.sender] = 0;  // fix
            bool success;
            bytes memory status;
            (success, status) = msg.sender.call.value(amount)(""); 
            require(success);
            credit[msg.sender] = 0;  // bug
        }
       uint bal = address(this).balance;
    }
}
