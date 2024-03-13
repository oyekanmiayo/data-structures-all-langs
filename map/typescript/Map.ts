type Entry<T extends string | number, V> = {
  key: T;
  value: V;
  next: Entry<T, V>;
};

class TsMap<T extends string | number, V> {
  private static readonly PRIME = 31;
  private entries: Array<Entry<T, V>>;
  private readonly len = 29;

  constructor() {
    this.entries = new Array(this.len);
  }

  put(key: T, value: V): void {
    let hash = this.getHashForKey(key);
    let newEntry = { key, value } as Entry<T, V>;

    // if there is no initial value at the beginning of the
    // bucket we just insert one
    if (!this.entries[hash]) {
      this.entries[hash] = newEntry;
      return;
    }

    let head = this.entries[hash];
    let prev: Entry<T, V> | null;

    while (head) {
      prev = head;
      if (prev.key === key) {
        throw new Error("Key already exists");
      }
      head = head.next;
    }

    prev!.next = newEntry;
  }
  get(key: T): V | undefined {
    let hash = this.getHashForKey(key);

    let head = this.entries[hash];

    while (head) {
      if (head.key === key) {
        return head.value;
      }

      head = head.next;
    }

    return;
  }
  delete(key: T): V | undefined {
    let hash = this.getHashForKey(key);

    let head = this.entries[hash];

    if (head?.key === key) {
      head = head.next;
      this.entries[hash] = head;
      return;
    }

    let prev: Entry<T, V>;

    while (head?.key !== key) {
      prev = head;
      head = head?.next;
    }

    prev!.next = head?.next;

    return;
  }

  size(): number {
    let size = 0;
    for (let i = 0; i < this.entries.length; i++) {
      let head = this.entries[i];

      while (head) {
        size++;
        head = head.next;
      }
    }

    return size;
  }

  private getHashForKey(key: T) {
    let stringifiedKey = key.toString();
    let sum = 0;

    for (let i = 0; i < stringifiedKey.length; i++) {
      sum += stringifiedKey.charCodeAt(i);
    }

    return (sum * TsMap.PRIME) % this.entries.length;
  }
}

let m = new TsMap<string, number>();

m.put("lies", 20);
m.put("foes", 53);
m.put("25", 25);

console.log(m.size());

m.delete("25");

console.log(m.size());

console.log(m.get("25"));
