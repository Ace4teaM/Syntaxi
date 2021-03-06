#ifndef _NP_L1_HANDLE
#define _NP_L1_HANDLE

#include <np.h>
#include <l1/chunkstack.h>

#define NP_HANDLE ushort
#define NP_HANDLE_NULL ((ushort)-1)

typedef struct _NP_HANDLE_INDICE{
	NP_HANDLE prev;
	NP_HANDLE next;
	ushort size;
	ushort order;
}NP_HANDLE_INDICE;

/**
	En-tete d'un handle
*/
typedef struct _NP_HANDLE_HEADER{
	ushort chunk_count;
	ushort chunk_size;
	size_t data_size;
	NP_HANDLE_INDICE* index;
	char* data;
	char* lock_data;
}NP_HANDLE_HEADER;

/**
	En-tete d'un handle 2
*/
typedef struct _NP_HANDLE_HEADER2{
	ushort chunk_count;
	ushort chunk_size;
	size_t data_size;
	NP_HANDLE_INDICE* index;
	char* data;
	char* lock_data;
}NP_HANDLE_HEADER2;

ushort    npInitHandle(ushort handle_count,ushort handle_size);
NP_HANDLE npFreeHandle();
NP_HANDLE npCreateHandle(void* data,size_t size);
size_t    npHandleSize(NP_HANDLE handle);
void*     npLockHandle(NP_HANDLE handle,void* data,size_t offset,size_t size);
NP_HANDLE npUnlockHandle(NP_HANDLE handle,void* data,size_t offset,size_t size);
int       npIsHandle(NP_HANDLE handle);
void*     npGetHandleLockBuffer();
size_t    npGetHandleLockBufferSize();

#endif
