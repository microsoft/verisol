pragma solidity >=0.4.24<0.6.0;
import "./Libraries/VeriSolContracts.sol";

contract SimpleDAO {
    uint balance;
    mapping (address => uint) public credit;
    constructor() public {
        balance = 100;
    }
    function donate(address to, uint amount) public {
        credit[to] += amount;
        balance += amount;
    }
    function queryCredit(address to) public view returns (uint) {
        return credit[to];
    }
    function queryBalance() public view returns (uint) {
        return balance;
    }
    function withdraw() public {
        if (credit[msg.sender] > 0) {
            uint amount = credit[msg.sender];
            balance -= amount;
            Mallory mallory = Mallory(msg.sender);
            mallory.fallback(amount);
            credit[msg.sender] = 0;
        }
        assert(balance == VeriSol.Old(balance - credit[msg.sender]));
    }
}

contract Mallory {
    SimpleDAO public dao;
    uint count;
    uint balance;
    constructor (address daoAddr) public {
        count = 0;
        dao = SimpleDAO(daoAddr);
        require(dao.queryCredit(address(this)) == 0);
        balance = 1;
    }
    function fallback(uint amount) public {
        require (amount >= 0);
        balance += amount;
        if (count < 3) {
            count ++;
            dao.withdraw();
        }
    }
    function donate() public {
        dao.donate(address(this), balance);
        balance -= balance;
    }
    function getJackpot() public {
        dao.withdraw();
    }
    function queryBalance() public view returns (uint) {
        return balance;
    }
}
