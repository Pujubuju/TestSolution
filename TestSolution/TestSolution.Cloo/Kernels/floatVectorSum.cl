__kernel void floatVectorSum(
	__global float * v1,
	__global float * v2)
{
	// Vector element index
	int i = get_global_id(0);
	v1[i] = v1[i] + v2[i];
}