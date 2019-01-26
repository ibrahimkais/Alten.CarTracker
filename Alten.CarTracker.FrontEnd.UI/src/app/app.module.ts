import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material';

import { HeadComponent } from './head/head.component';
import { FooterComponent } from './footer/footer.component';
import { MapComponent } from './map/map.component';
import { RouteGridComponent } from './route-grid/route-grid.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { AboutComponent } from './about/about.component';
import { DataFilterComponent } from './data-filter/data-filter.component';
import { SidenavService } from './services/SideNavService';

@NgModule({
  declarations: [
    AppComponent,
    HeadComponent,
    FooterComponent,
    MapComponent,
    RouteGridComponent,
    VehicleComponent,
    AboutComponent,
    DataFilterComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule
  ],
  providers: [SidenavService],
  bootstrap: [AppComponent]
})
export class AppModule { }
