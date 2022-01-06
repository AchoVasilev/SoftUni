function createComputerHierarchy() {
    class Component {
        constructor(manufacturer) {
            if (this.constructor === Component) {
                throw new Error('Cannot instantiate an abstract class');
            }

            this.manufacturer = manufacturer;
        }
    }

    class Keyboard extends Component {
        constructor(manufacturer, responseTime) {
            super(manufacturer);
            this.responseTime = responseTime;
        }
    }

    class Monitor extends Component {
        constructor(manufacturer, width, height) {
            super(manufacturer);
            this.width = width;
            this.height = height;
        }
    }

    class Battery extends Component {
        constructor(manufacturer, expectedLife) {
            super(manufacturer);
            this.expectedLife = expectedLife;
        }
    }

    class Computer extends Component {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace) {
            // @ts-ignore
            if (this.constructor === Computer) {
                throw new Error('Cannot instantiate an abstract class');
            }

            super(manufacturer);
            this.processorSpeed = processorSpeed;
            this.ram = ram;
            this.hardDiskSpace = hardDiskSpace;
        }
    }

    class Laptop extends Computer {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace, weight, color, battery) {
            super(manufacturer, processorSpeed, ram, hardDiskSpace);
            this.weight = weight;
            this.color = color;
            this.battery = battery;
        }

        get battery() {
            return this._battery;
        }
        set battery(value) {
            if (!(value.instanceOf(Battery))) {
                throw new TypeError('Value is not an instance of the Battery class');
            }

            this._battery = value;
        }
    }

    class Desktop extends Computer {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace, keyboard, monitor) {
            super(manufacturer, processorSpeed, ram, hardDiskSpace);
            this.keyboard = keyboard;
            this.monitor = monitor;
        }

        get keyboard() {
            return this._keyboard;
        }
        set keyboard(value) {
            if (!(value.instanceOf(Keyboard))) {
                throw new TypeError('Value is not an instance of the Keyboard class');
            }

            this._keyboard = value;
        }

        get monitor() {
            return this._monitor;
        }
        set monitor(value) {
            if (!(value.instanceOf(Monitor))) {
                throw new TypeError('Value is not an instance of the Monitor class');
            }

            this._monitor = value;
        }
    }
}