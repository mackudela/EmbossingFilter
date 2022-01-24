public RunAsm

;RunAsm proc a: QWORD, b: QWORD, c: DWORD, d: DWORD, e: DWORD, f: DWORD
.code

; RunCpp(byte[] outputArray, byte[] maskArray, int startingPoint
; int finishPoint, int width, int height);

RunAsm proc a: QWORD, b: QWORD, c: DWORD, d: DWORD, e: DWORD, f: DWORD
; a(outputArray) -> rcx, b(originalArray) -> rdx, c(startingPoint) -> r8
; d(finishPoint) -> r9,  e(width) -> stack,       f(height) -> stack

	push rbx
	push r12
	push r13
	push r14 
	push r15

	;=================================================================

	mov r10, [rbp+48] ; moving e(width) to r10
	mov r11, [rbp+56] ; moving f(height) to r11
	mov r12, rcx ; moving output array to r12
	mov r13, rdx ; moving original array to r13
	mov rax, r8 ; moving currentPixel to rax
	xor rcx, rcx ; rcx = 0
	
	mov al, 127 ; al = 127
	movd xmm3, eax ; xmm3 = [127, 0, 0, 0]
	pinsrd xmm3, eax, 1 ; xmm3 = [127, 127, 0, 0]
	pinsrd xmm3, eax, 2 ; xmm3 = [127, 127, 127, 0]

	;=================================================================
	; r12  -> outputArray
	; r13  -> originalArray
	; r8   -> startingPoint (currentPixel)
	; r9   -> finishPoint
	; r10  -> width
	; r11  -> height
	; xmm3 -> [127, 127, 127, 0]
	;=================================================================

	dec r11 ; decrementing r11, which now equals height-1
	mov rax, 3 ; copying 3 to rax
	mul r10 ; multiplying width * 3
	mov r10, rax ; moving width * 3 to r10

	;=================================================================
	; Check for border pixels 
	;=================================================================

borderCheck:
	xor rax, rax ; rax = 0
	xor rdx, rdx ; rdx = 0
	mov rax, r8 ; moving current pixel to rax
	div r10 ; dividing current pixel by (width*3)
	cmp rax, 0 ; compare if currentPixel / (width*3) == 0
	je skipPixel ; skip if it's in first row
	cmp rax, r11 ; compare if currentPixel / (width*3) == height - 1
	je skipPixel ; skip if it's in last row
	cmp rdx, 0 ; compare currentPixel % (width*3) == 0
	je skipPixel ; skip if it's in first column
	xor rax, rax ; rax = 0
	xor rdx, rdx ; rdx = 0
	mov rax, r8	; moving current pixel to rax
	add rax, 3 ; adding 3 to rax, looking for next pixel (RGB)
	div r10 ; dividing (currentPixel + 3) / (width*3) 
	cmp rdx, 0 ; comparing remainder to zero
	je skipPixel ; if it's in last column
	jmp applyFilter ; if it's not a border pixel, go to filter

skipPixel:
	add r8, 3 ; go to next pixel (3 as each pixel is represented by RGB) 
	cmp r8, r9 ; check if current pixel is still in range for this thread
	jge finish ; if out of range, finish
	jmp borderCheck ; if not out of range, continue
	
applyFilter:
	xor rax, rax ; rax = 0

	mov al, byte ptr[r13 + r8 - 3] ; value of previousPixel (G)
	movd xmm1, eax ; xmm1 = [G, 0, 0, 0]
	mov al, byte ptr[r13 + r8 - 2] ; value of previousPixel (B)
	pinsrd xmm1, eax, 1 ; xmm1 = [G, B, 0, 0]
	mov al, byte ptr[r13 + r8 - 1] ; value of previousPixel (R)
	pinsrd xmm1, eax, 2 ; xmm1 = [G, B, R, 0]

	mov al, byte ptr[r13 + r8 + 3] ; value of nextPixel (G)
	movd xmm2, eax ; xmm2 = [G, 0, 0, 0]
	mov al, byte ptr[r13 + r8 + 4] ; value of nextPixel (B)
	pinsrd xmm2, eax, 1 ; xmm2 = [G, B, 0, 0]
	mov al, byte ptr[r13 + r8 + 5] ; value of nextPixel (R)
	pinsrd xmm2, eax, 2 ; xmm2 = [G, B, R, 0]

	psubd xmm2, xmm1 ; xmm2 = [G2-G1, B2-B1, R2-R1, 0]
	paddd xmm2, xmm3 ; xmm2 = [G2-G1+127, B2-B1+127, R2-R1+127, 0]

	pextrd eax, xmm2, 0 ; extract new G from xmm2
	mov [r12 + r8], al ; currentPixel.G = new G
	pextrd eax, xmm2, 1 ; extract new B from xmm2
	mov [r12 + r8 + 1], al ; currentPixel.B = new B
	pextrd eax, xmm2, 2 ; extract new R from xmm2
	mov [r12 + r8 + 2], al ; currentPixel.R = new R

	jmp skipPixel

finish:
	pop r15
	pop r14
	pop r13
	pop r12
	pop rbx
	ret

RunAsm endp

end