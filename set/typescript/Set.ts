type Entry<V extends string | number> = {
  value: V;
  next: Entry<V>;
};

class TsSet<V extends string | number> {
  private static readonly PRIME = 31;
  private entries: Array<Entry<V>>;
  private readonly len = 29;

  constructor() {
    this.entries = new Array(this.len);
  }

  add(value: V): void {
    let hash = this.getHashForKey(value);
    let newEntry = { value } as Entry<V>;

    // if there is no initial value at the beginning of the
    // bucket we just insert one
    if (!this.entries[hash]) {
      this.entries[hash] = newEntry;
      return;
    }

    let head = this.entries[hash];
    let prev: Entry<V> | null;

    while (head) {
      prev = head;
      if (prev.value === value) {
        throw new Error("Duplicate value detected");
      }
      head = head.next;
    }

    prev!.next = newEntry;
  }
  contains(value: V): boolean {
    let hash = this.getHashForKey(value);
    let head = this.entries[hash];

    while (head) {
      if (head.value === value) {
        return true;
      }

      head = head.next;
    }

    return false;
  }
  remove(value: V): V | undefined {
    let hash = this.getHashForKey(value);
    let head = this.entries[hash];

    if (head?.value === value) {
      this.entries[hash] = head.next;
      return head.value;
    }

    let prev: Entry<V>;

    while (head?.value !== value) {
      prev = head;
      head = head?.next;
    }

    prev!.next = head?.next;

    return head.value;
  }

  list() {
    let entries: V[] = [];

    for (let i = 0; i < this.entries.length; i++) {
      let head = this.entries[i];

      while (head) {
        entries.push(head.value);
        head = head.next;
      }
    }

    return entries;
  }

  private getHashForKey(key: V) {
    let stringifiedKey = key.toString();
    let sum = 0;

    for (let i = 0; i < stringifiedKey.length; i++) {
      sum += stringifiedKey.charCodeAt(i);
    }

    return (sum * TsSet.PRIME) % this.entries.length;
  }
}

let m = new TsSet<number>();

m.add(2);
m.add(4);
m.add(8);
m.add(10);
m.add(12);

console.log(m.list());
console.log(m.remove(2));
console.log(m.remove(12));
console.log(m.list());
console.log(m.contains(2));
console.log(m.contains(4));
console.log(m.contains(12));
