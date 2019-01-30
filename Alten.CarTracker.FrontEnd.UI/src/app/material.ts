import {
    MatButtonModule,
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,
    MatTableModule,
    MatSelectModule,
    MatToolbarModule,
    MatDividerModule,
    MatFormFieldModule,
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
        ReactiveFormsModule,
        MatDividerModule,
        MatFormFieldModule,
    ],
    exports: [
        MatButtonModule,
        MatExpansionModule,
        MatSidenavModule,
        MatIconModule,
        MatTableModule,
        MatSelectModule,
        MatToolbarModule,
        ReactiveFormsModule,
        MatDividerModule,
        MatFormFieldModule,
    ],
})

export class MaterialModule { }
