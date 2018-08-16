import {Injectable} from '@angular/core';
import {Catalog} from '../data/models/catalog/catalog';
import {CatalogGood} from "../data/models/products/catalogGood";
import {CatalogGoodSave} from "../data/models/products/catalogGoodSave";
import {LetterObject} from "../data/models/letterObject";

@Injectable()

export class DateConverterService {

  setDateMin(date: number) {
    const dateMin = new Date(date, 0, 1, 0, 0, 1).toDateString();
    return dateMin;
  }

  setDateMax(date: number) {
    const dateMax = new Date(date, 11, 31, 22, 58, 58).toDateString();
    return dateMax;
  }

  dateValidity(catalogGoods: CatalogGoodSave[]) {
    let i: number = 0;
    catalogGoods.forEach(item => {
      if (item.dateMin == null || item.dateMax == null || +item.dateMin > +item.dateMax) {
        i = i + 1;
      }
    })
    return i;
  }

  dateValidityForDateArray(dateArr: LetterObject[]) {
    let i: number = 0;
    dateArr.forEach(item => {
      if (item.dateMin == null || item.dateMax == null || +item.dateMin > +item.dateMax) {
        i = i + 1;
      }
    })
    return i;
  }

  dateDisplay(dateArr: any[], catalogGoods: CatalogGood[]) {

    dateArr.forEach(item => {
      item.dateMin = catalogGoods.filter(x => x.indexId === item.letter && x.isAddedManually === false).length > 0 ?
        +catalogGoods.filter(x => x.indexId === item.letter)[0].dateMin.substring(0, 4) : null;

      item.dateMax = catalogGoods.filter(x => x.indexId === item.letter && x.isAddedManually === false).length > 0 ?
        +catalogGoods.filter(x => x.indexId === item.letter)[0].dateMax.substring(0, 4) : null;
    })

    catalogGoods.filter(x => x.isAddedManually === true).forEach(item =>  {
      item.dateMin = item.dateMin.substring(0, 4);
      item.dateMax = item.dateMax.substring(0, 4);
    })
  }

  dateToYear(date: string): string {

    const year: string = date.substring(0, 4);
    return year;
  }

  getLastDate() {
   const dateArr: any[] = [];

    for (let i = 0; i <= 18; i++) {
      let date: number = 0;
      const newDate: string = new Date().toString().substring(15, 4);
      date = +newDate.substr(newDate.length - 4) - i;
      const dateObj = {
        title: date,
        value: date,
      };
      dateArr.push(dateObj);
    }
    return dateArr;
  }

}
