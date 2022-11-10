/*
 * SmartBatteryTesterMCU.c
 *
 * Created: 10.11.2022 12:48:54
 * developer: vasileuski.sergei@gmail.com
 */ 


#define F_CPU 8000000L
#include <avr/io.h>
#include <avr/interrupt.h>
#include <util/delay.h>
#include <stdio.h>
#include <stdlib.h>
#include <avr/interrupt.h>

#define E1 PORTD |= 0b00001000; // � - �����
#define E0 PORTD &= 0b11110111;
#define RS1 PORTD |= 0b00000100; // ������
#define RS0 PORTD &= 0b11111011; // �������

//----------- ADConv -----------

void AdcInit()
{
	ADCSRA |= (1 << ADEN) | (1 << ADPS2) | (1 << ADPS1);
	ADMUX &= ~(1 << REFS1);
	ADMUX |= (1 << REFS0);
}

//-----------------------------

//----------- Display -----------

void PortForDisplayInit()
{
	DDRD = 0xFF;
	PORTD = 0x00;
}

void SendHalfByte(unsigned char halfByte)
{
	halfByte <<= 4; // �������� ������ �� �������� ���������
	E1;
	_delay_us(50);
	PORTD &= 0b00001111;
	PORTD |= halfByte;
	E0;
	_delay_us(50);
}

void SendByte(unsigned char data, unsigned char mode)
{
	if (mode == 0)
	{
		RS0;
	}
	else
	{
		RS1;
	}
	
	unsigned char hData = 0;
	hData = data >> 4;
	
	SendHalfByte(hData);
	SendHalfByte(data);
}

void SendChar(unsigned char symbol)
{
	SendByte(symbol, 1);
}

void SetPos(unsigned char x, unsigned char y)
{
	char address;
	address = (0x40 * y + x) | 0b10000000;
	SendByte(address, 0);
}

void ClearDisplay()
{
	SendByte(0b00000001, 0);
	_delay_us(1500);
}

void SendString (char str[])
{
	wchar_t i;
	for(i = 0; str[i] != '\0'; i++)
	{
		SendChar(str[i]);
	}
}

void DisplayInit()
{
	PortForDisplayInit();
	_delay_ms(15);
	
	// �������� 4-� ������ �����: 3 ���� �������� 11, ���� ��� ��� ���� - 10
	SendHalfByte(0b00000011);
	_delay_ms(4);
	SendHalfByte(0b00000011);
	_delay_us(100);
	SendHalfByte(0b00000011);
	_delay_ms(1);
	SendHalfByte(0b00000010); // ��������, �������� �������� ��� ����
	_delay_ms(1);
	
	SendByte(0b00101000, 0); // 4-� ������ ����� (DL=0) � 2 ����� (N=1)
	_delay_ms(1);
	
	SendByte(0b00001100, 0); // ��� ����� (D=1) ��� �������� (C=0, B=0)
	_delay_ms(1);
	
	SendByte(0b00000110, 0); // �������� ������� �����
	_delay_ms(1);
	
	ClearDisplay();
}

//-----------------------------

//----------- USART -----------

void USART_Init(unsigned int speed)
{
	UBRRH = (unsigned char)(speed >> 8);
	UBRRL = (unsigned char)speed;
	
	UCSRB |= (1 << RXEN) | (1 << TXEN); // ������. ����� ��������
	UCSRB |= (1 << RXCIE); // ���. ����������
	UCSRA |= (1 << U2X); // 8 ���
	UCSRC |= (1 << URSEL) // ��������� � UCSRC
	| (1 << UCSZ1)
	| (1 << UCSZ0); // UCSZ - 8 ���
	//| (UMSEL = 0) //- ����������� �����
	//| (UPM1 = 0) // UPM1 = 0 � UPM0 = 0 - ��� �������� ��������
	//| (UPM0 = 0);
}

void UsartDataTransmit(unsigned char data)
{
	while (!(UCSRA & (1 << UDRE)));
	UDR = data;
	// TODO do something with data
}

void UsartDataArrayTransmit (char str[])
{
	wchar_t i;
	for(i = 0; str[i] != '\0'; i++)
	{
		UsartDataTransmit(str[i]);
	}
	
	UsartDataTransmit(0x0a);
}
//-----------------------------


ISR(USART_RXC_vect)
{
	int dataFromUsart = 0;
	dataFromUsart = UDR;
	
	if (dataFromUsart == 49)
	{
		PORTB &= ~(1<<(PORTB0));
	}
	else if (dataFromUsart == 50)
	{
		PORTB |= (1<<(PORTB0));
	}
}


int main(void)
{
	DDRB = 0xFF;
	AdcInit();
	DisplayInit();
	USART_Init(16); // 57 600
	sei();
	
	// DAC
	float result = 0;
	unsigned short adc_value = 0;
	char buffer [30];
	char resultToUsart [10];

	SetPos(0, 0);
	
	while(1)
	{	// DAC
		ADCSRA |= (1 << ADSC);
		while((ADCSRA & (1<<ADSC)));
		adc_value = (unsigned short) ADC;
		result = (float)adc_value / (float)204.2689; // 1024 / 5 = 204,8; 5 - ������� ����.
		ClearDisplay();
		sprintf(buffer,"Ubat = %.2f V", result);
		SendString(buffer);

		sprintf(resultToUsart,"%.1f", result);
		UsartDataArrayTransmit(resultToUsart);
		
		_delay_ms(500);
		// TODO work with ADC using interruptions

	}
}