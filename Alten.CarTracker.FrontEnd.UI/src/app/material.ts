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
    MatTooltipModule,
} from '@angular/material';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
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
        MatTooltipModule,
        FormsModule,
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
        MatTooltipModule,
        FormsModule,
    ],
})

export class MaterialModule { }
