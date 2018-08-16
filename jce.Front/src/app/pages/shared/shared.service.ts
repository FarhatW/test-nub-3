import {Injectable} from "@angular/core";
import {BehaviorSubject} from "rxjs/BehaviorSubject";

@Injectable()
export class SharedService {

  perPage: number;
  private perPageSource = new BehaviorSubject<number>(this.perPage);

  currentPerPage =  this.perPageSource.asObservable();

  setPerpage(perPage: number) {
    this.perPageSource.next(perPage);

  }

  getPerPage(): number {
    return this.perPageSource.getValue();
  }


}
