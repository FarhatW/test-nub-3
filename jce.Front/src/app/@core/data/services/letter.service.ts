import { Injectable} from '@angular/core';
import {Http} from '@angular/http';
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {LetterEnum} from "../models/Enums/letter.enum";
import {BehaviorSubject} from "rxjs/BehaviorSubject";
import {KeyValuePairEnum} from "../models/Enums/keyValuePair.enum";


@Injectable()
export class LetterService {

  constructor(private  http: HttpClient) {
  }

  letters: LetterEnum[];
  private lettersSource = new BehaviorSubject<LetterEnum[]>(this.letters);
  currentLetters = this.lettersSource.asObservable();

  // Pass data to siblings components

  changeLetters(letters: LetterEnum[]) {
    this.lettersSource.next(letters);
  }

  getCurrentLetter(): LetterEnum[] {
    return this.lettersSource.getValue()
  }



  url = environment.apiUrl;


  private readonly letterEndPoint = this.url + 'letterindex/';

  getAll() {
    return this.http.get<LetterEnum[]>(this.letterEndPoint);
  }

  getById(id: number) {
    return this.http.get(this.letterEndPoint + id)
      .map(res => res);
  }

  getByName(letter: string) {
    return this.http.get(this.letterEndPoint + letter)
      .map(res => res);
  }
}
