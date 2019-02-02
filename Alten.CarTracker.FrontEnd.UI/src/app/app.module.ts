import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material';
import { HttpClientModule } from '@angular/common/http';

import { HeadComponent } from './head/head.component';
import { FooterComponent } from './footer/footer.component';
import { MapComponent } from './map/map.component';
import { RouteGridComponent } from './route-grid/route-grid.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { DataFilterComponent } from './data-filter/data-filter.component';
import { SidenavService } from './services/SideNavService';
import { CarsFilterPipe } from './data-filter/CarsFilterPipe';
import { TableDataSourcePipe } from './route-grid/table-data-source.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HeadComponent,
    FooterComponent,
    MapComponent,
    RouteGridComponent,
    VehicleComponent,
    DataFilterComponent,
    CarsFilterPipe,
    TableDataSourcePipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule
  ],
  providers: [SidenavService],
  bootstrap: [AppComponent]
})
export class AppModule { }
