class List {
    constructor() {
        this.numbersArr = [];
        this.size = this.numbersArr.length;
    }

    updateSize = () => this.size = this.numbersArr.length;
    sortList = () => this.numbersArr.sort((a, b) => a - b);

    add(element) {
        this.numbersArr.push(element);
        this.updateSize();
        this.sortList();
    }

    remove(index) {
        if (this.numbersArr[index] !== undefined) {
            this.numbersArr.splice(index, 1);
            this.updateSize();
            this.sortList();
        }
    }

    get(index) {
        if(this.numbersArr[index] !== undefined) {
            this.updateSize();
            this.sortList();
            
            return this.numbersArr[index];
        }
    }
}

let list = new List();
list.add(5);
list.add(6);
list.add(7);
console.log(list.get(1)); 
list.remove(1);
console.log(list.get(1));