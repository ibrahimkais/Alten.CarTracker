import { Pipe, PipeTransform } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

@Pipe({
  name: 'tableDataSource'
})
export class TableDataSourcePipe implements PipeTransform {

  transform(array: any[]): any {
    return array ? new MatTableDataSource(array) : new MatTableDataSource([]);
  }

}
