pragma solidity >=0.4.24<0.6.0;

contract SimpleDAO {
    mapping (address => uint) public credit;
    constructor() public {
    }
    function donate(address to, uint amount) public {
        credit[to] += amount;
    }
    function queryCredit(address to) public view returns (uint) {
        return credit[to];
    }
    function withdraw() public {
        uint oldBal = 0; //uint oldBal = address(this).balance; //FIX
        uint amount = credit[msg.sender];
        if (amount > 0) {
            // msg.sender.transfer(amount); // FIX
            credit[msg.sender] = 0;
        }
        uint bal = 0; // address(this).balance;
        assert(bal == oldBal || bal == (oldBal - amount));
    }
}

contract Mallory {
    SimpleDAO public dao;
    uint count;
    constructor (address daoAddr) public {
        count = 0;
        dao = SimpleDAO(daoAddr);
        require(dao.queryCredit(address(this)) == 0);
        // require(address(this).balance == 1);
    }
    function() external {
        if (count < 3) {
            count ++;
            dao.withdraw();
        }
    }
    function donate() public {
        // dao.donate(address(this), address(this).balance); //FIX
    }
    function getJackpot() public {
        dao.withdraw();
    }
    function queryBalance() public view returns (uint) {
        return 0; // return address(this).balance; //FIX
    }
}
