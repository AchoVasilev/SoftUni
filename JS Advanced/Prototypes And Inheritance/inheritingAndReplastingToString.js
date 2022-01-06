function toStringExtension() {
    class Person {
        constructor(name, email) {
            this.name = name;
            this.email = email;
        }

        toString() {
            return `${this.constructor.name} (name: ${this.name}, email: ${this.email})`
        }
    }

    class Teacher extends Person {
        constructor(name, email, subject) {
            super(name, email);
            this.subject = subject;
        }

        toString() {
            let str = super.toString();
            let bracket = super.toString().slice(-1);
            str = str.slice(0, str.length - 1);
            return str + ', ' + `subject: ${this.subject}${bracket}`;
        }
    }

    class Student extends Person {
        constructor(name, email, course) {
            super(name, email);
            this.course = course;
        }

        toString() {
            let str = super.toString();
            let bracket = super.toString().slice(-1);
            str = str.slice(0, str.length - 1);
            return str + ', ' + `course: ${this.course}${bracket}`;
        }
    }

    return {
        Person,
        Teacher,
        Student
    };
}

toStringExtension();