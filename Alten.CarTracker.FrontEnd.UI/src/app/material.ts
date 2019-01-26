import {
    MatButtonModule,
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,
    MatTableModule,
    MatSelectModule,
    MatToolbarModule,
 } from '@angular/material';
 import {ReactiveFormsModule} from '@angular/forms';
import { NgModule } from '@angular/core';

@NgModule({
    imports: [
        MatButtonModule,
        MatExpansionModule,
        MatSidenavModule,
        MatIconModule,
        MatTableModule,
        MatSelectModule,
        MatToolbarModule,
        ReactiveFormsModule
    ],
    exports: [
        MatButtonModule,
        MatExpansionModule,
        MatSidenavModule,
        MatIconModule,
        MatTableModule,
        MatSelectModule,
        MatToolbarModule,
        ReactiveFormsModule
    ],
})

export class MaterialModule { }
