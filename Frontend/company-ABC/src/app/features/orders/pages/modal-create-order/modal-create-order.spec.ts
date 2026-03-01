import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalCreateOrder } from './modal-create-order';

describe('ModalCreateOrder', () => {
  let component: ModalCreateOrder;
  let fixture: ComponentFixture<ModalCreateOrder>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalCreateOrder]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalCreateOrder);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
